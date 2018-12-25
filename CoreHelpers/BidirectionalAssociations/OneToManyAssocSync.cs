using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace HappyNomad.BidirectionalAssociations {
	/// <summary>
	/// Keeps both sides of a bidirectional one-to-many association in sync with each other.
	/// </summary>
	/// <author>Adrian Alexander</author>
	public sealed class OneToManyAssocSync {
		private object thisOneSide;

		/// <param name="thisOneSide">The entity participating in the association which has a multiplicity of one</param>
		/// <param name="manyToOnePropertyName"></param>
		public OneToManyAssocSync( object thisOneSide, string manyToOnePropertyName ) {
			this.thisOneSide = thisOneSide;
			this.manyToOnePropertyName = manyToOnePropertyName;
		}

		/// <summary>
		/// Responds to add/remove events raised by the one-side's collection.
		/// </summary>
		public void UpdateManySide( object sender, NotifyCollectionChangedEventArgs e ) {
			if ( e.Action == NotifyCollectionChangedAction.Add ) {
				//addingToManySide: the item that was just added to this one-side's collection
				foreach ( object addingToManySide in e.NewItems )
					if ( addingToManySide != null && NavigateManyToOne(addingToManySide) != thisOneSide )
						SetManyToOne( addingToManySide, thisOneSide );
			} else if ( e.Action == NotifyCollectionChangedAction.Remove ) {
				//removingFromManySide: the item that was just removed from this one-side's collection
				foreach ( object removingFromManySide in e.OldItems )
					if ( NavigateManyToOne( removingFromManySide ) == thisOneSide )
						SetManyToOne( removingFromManySide, null );
			}
		}

		/// <summary>
		/// Responds to a many-side entity's parent one-side property being set to a new value.
		/// </summary>
		public static void UpdateOneSide<T>( T thisManySide, object oldOneSide, object newOneSide, string oneToManyPropertyName ) {
			if ( oldOneSide != null && oldOneSide != newOneSide ) {
				ICollection<T> oldCollection =
				  ReflectionUtil.NavigateToManySide<T>( oldOneSide, oneToManyPropertyName );
				if ( oldCollection.Contains(thisManySide) ) {
					Console.WriteLine( "\tremoving from old parent collection" );
					oldCollection.Remove( thisManySide );
					if ( oldCollection is IList && oldCollection.Contains(thisManySide) ) // true if there was more than one
						throw new InvalidOperationException( "Collection is non-unique" );
				}
			} 
			if ( newOneSide != null ) {
				ICollection<T> newCollection =
				  ReflectionUtil.NavigateToManySide<T>( newOneSide, oneToManyPropertyName );
				if ( ReflectionUtil.IsInitialized(newCollection) && !newCollection.Contains(thisManySide) ) {
					Console.WriteLine( "\tadding to new parent collection" );
					if ( newCollection is IList && newCollection.Contains(thisManySide) ) // true if add will cause duplicates
						throw new InvalidOperationException( "Collection is non-unique" );
					newCollection.Add( thisManySide );
				}
			}
		}

		#region Many-to-One Navigation

		private string manyToOnePropertyName;
		private PropertyInfo manyToOneProperty;

		private object NavigateManyToOne( object manySide ) {
			return GetManyToOneProperty( manySide.GetType() ).GetValue( manySide, null );
		}

		private void SetManyToOne( object manySide, object newValue ) {
			GetManyToOneProperty( manySide.GetType() ).SetValue( manySide, newValue, null );
		}

		private PropertyInfo GetManyToOneProperty( Type manySideType ) {
			if ( manyToOneProperty == null ) {
				PropertyInfo pi = manySideType.GetProperty( manyToOnePropertyName );
				manyToOneProperty = pi.DeclaringType.GetProperty( manyToOnePropertyName );
			}
			return manyToOneProperty;
		}

		#endregion
	
	}
}
