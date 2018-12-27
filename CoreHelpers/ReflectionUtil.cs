using System;
using System.Collections.Generic;
using System.Reflection;

namespace HappyNomad {

	public static class ReflectionUtil {

		public static object NavigateToOneSide( object start, string propName ) {
			PropertyInfo pi = start.GetType().GetProperty( propName );
			return pi.GetValue( start, null );
		}

		public static ICollection<T> NavigateToManySide<T>( object start, string propName ) {
			BindingFlags bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			PropertyInfo pi = start.GetType().GetProperty( propName, bf )
				.DeclaringType.GetProperty( propName, bf );
			return (ICollection<T>)pi.GetValue( start, null );
		}

		#region 'IsInitialized' Method

		private static MethodInfo isInitialized;

		public static bool IsInitialized<T>( ICollection<T> newCollection ) {
			if ( isInitialized == null ) {
				Type t = Type.GetType( "NHibernate.NHibernateUtil, NHibernate" );
				if ( t != null )
					isInitialized = t.GetMethod( "IsInitialized", BindingFlags.Static | BindingFlags.Public );
			}
			if ( isInitialized != null ) // true if the NHibernate assembly is present
				return (bool)isInitialized.Invoke( null, new object[] { newCollection } );
			else
				return true;
		}

		#endregion

	}
}
