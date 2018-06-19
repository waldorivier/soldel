using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mupeModel.Utils {
    enum TokenType : int { LITTERAL = 1, SEPARATOR, OPERATOR, NULL };

    internal class Token {

        private TokenType type;
        private StringBuilder name = new StringBuilder();

        // reference sur le Token suivant dans la représentation "liste"
        private Token nextList;

        // reference sur le Token précèdent dans la représentation "liste"
        private Token prevList;

        // reference sur le Token suivant dans la représentation "tree"
        private Token nextTree;

        // reference sur la valeur du Token courant 
        private Token valueTree;

        // indique le niveau de profondeur du Token dans l'arbre
        private String levelSeparator;
        public void IncrementLevelSeparator() {
            levelSeparator = ((char)21).ToString() + levelSeparator;
        }

        public StringBuilder Name { get => name; set => name = value; }

        public Token NextList { get => nextList; set => nextList = value; }
        public Token PrevList { get => prevList; set => prevList = value; }
        public Token NextTree { get => nextTree; set => nextTree = value; }
        public Token ValueTree { get => valueTree; set => valueTree = value; }
        public String LevelSeparator { get => levelSeparator; set => levelSeparator = value; }
        public TokenType Type { get => type; set => type = value; }

        public Token() {
        }

        public override string ToString() {
            return "Name => " + Name.ToString() + " => Type = " + Type;
        }

        // détermine si le token courant possède une ... descendance
        public Boolean hasSubTree() {
            Boolean has = false;
            if (valueTree != null) {
                has = (valueTree.valueTree != null);
            }
            return has;
        }
    }

    internal class Grammar {
        private List<char> separators;
        private char endOfExpression;
        private List<char> operators;
        private List<char> literalDelimiters;

        public Grammar() {
            separators = new List<char> { (char)21, (char)27 };
            endOfExpression = (char)172;
            operators = new List<char> { ('=') };
            literalDelimiters = Separators.Union(Operators).ToList();
            literalDelimiters.Add(EndOfExpression);
        }

        public List<char> Separators { get => separators; }
        public List<char> Operators { get => operators; }
        public List<char> Literal_delimiters { get => literalDelimiters; }
        public char EndOfExpression { get => endOfExpression; }
    }
    

    class Parser {
        // parser une liste selon une grammaire; chainage avant et arrière des tokens de la liste
        public IList<Token> parse(StringBuilder s, Grammar grammar) {
            var tokens = new List<Token>();
            Token prevToken = null;

            // sentinelle de fin d'expression
            s.Append(grammar.EndOfExpression);

            int i = 0;
            while (true) {
                Token token = new Token();
                if (prevToken != null) {
                    prevToken.NextList = token;
                }
                token.PrevList = prevToken;
                prevToken = token;

                char c = s[i];

                if (!grammar.Literal_delimiters.Contains(c)) {
                    token.Type = TokenType.LITTERAL;
                    while (!grammar.Literal_delimiters.Contains(c)) {
                        token.Name.Append(c);
                        c = s[++i];
                    }
                } else {
                    if (grammar.Separators.Contains(c)) {
                        token.Type = TokenType.SEPARATOR;
                        while (grammar.Separators.Contains(c)) {
                            token.Name.Append(c);
                            c = s[++i];
                        }
                    } else {
                        if (grammar.Operators.Contains(c)) {
                            token.Type = TokenType.OPERATOR;
                            token.Name.Append(c);
                            c = s[++i];
                        } else {
                            if (c == grammar.EndOfExpression) {
                                break;
                            }
                        }
                    }
                }
                tokens.Add(token);
            }
            return tokens;
        }
    }
}
