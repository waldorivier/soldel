using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using mupeModel.Utils;

namespace mupeModel.Utils {
    public class TokenTree {
        private IList<Token> tokens;
        private Stack<Token> stack = new Stack<Token>();
        internal IList<Token> Tokens { get => tokens; set => tokens = value; }

        public TokenTree() {
        }

        public void build() {
            if (tokens != null) {
                stack.Clear();
                Tokens[0].LevelSeparator = ((char)27).ToString();
                Evaluate(Tokens[0]);
            } else {
                throw new Exception("La liste des éléments doit être disponible");
            }
        }

        private void Evaluate(Token token) {
            if (token != null) {
                switch (token.Type) {
                    case TokenType.LITTERAL:
                        stack.Push(token);
                        Evaluate(token.NextList);
                        break;

                    case TokenType.SEPARATOR:
                        Token LitteralToken = stack.Pop();
                        while (stack.Count > 0) {
                            LitteralToken = stack.Peek();
                            if (LitteralToken.LevelSeparator.Equals(token.Name.ToString())) {
                                LitteralToken = stack.Pop();
                                break;
                            }
                            LitteralToken = stack.Pop();
                        }
                        token.NextList.LevelSeparator = LitteralToken.LevelSeparator;
                        LitteralToken.NextTree = token.NextList;
                        Evaluate(token.NextList);
                        break;

                    case TokenType.OPERATOR:
                        token.NextList.LevelSeparator = token.PrevList.LevelSeparator;
                        token.NextList.IncrementLevelSeparator();
                        if (token.NextList.Type.Equals(TokenType.SEPARATOR)) {
                            stack.Push(new Token() { Name = new StringBuilder("NULL"), Type = TokenType.NULL });
                        } else {
                            token.PrevList.ValueTree = token.NextList;
                        }
                        Evaluate(token.NextList);
                        break;

                    default:
                        break;
                }
            }
        }

        // retourne un dictionnaire formé de (key=Name, value=Value)
        // dont les clefs sont uniques

        public IDictionary<string, string> asDictionary() {
            var dict = new Dictionary<string, string>();

            foreach (Token token in Tokens) {
                if (dict.ContainsKey(token.Name.ToString())) {
                } else {
                    if (!token.hasSubTree()) {
                        if (token.ValueTree != null)
                            if (token.Type == TokenType.LITTERAL) {
                                dict.Add(token.Name.ToString(), token.ValueTree.Name.ToString());
                            }
                    } else {
                        dict.Add(token.Name.ToString(), "NULL");
                    }
                }
            }
            return dict;
        }

        public ExpandoObject asObject() {
            dynamic obj = new ExpandoObject();

            IDictionary<string, string> properties = this.asDictionary();
            foreach (var property in properties) {
                AddProperty(obj, property.Key.ToString(), property.Value);
            }

            return obj;
        }

        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue) {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}