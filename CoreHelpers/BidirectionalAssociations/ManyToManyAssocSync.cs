using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace HappyNomad.BidirectionalAssociations {
	/// <summary>
	/// Keeps both sides of a bidirectional many-to-many association in sync with each other.
	/// </summary>
	/// <author>Adrian Alexander</author>
	public sealed class ManyToManyAssocSync<T> {
		private T thisSide;
		private string otherSidePropertyName;
		private PropertyInfo otherSideProperty;

		public ManyToManyAssocSync( T thisSide, string otherSideToThisSidePropertyName ) {
			this.thisSide = thisSide;
			this.otherSidePropertyName = otherSideToThisSidePropertyName;
		}

		/// <summary>
		/// Responds to add/remove events raised by this side's collection.
		/// </summary>
		public void UpdateOtherSide( object sender, NotifyCollectionChangedEventArgs e ) {
			if ( e.Action == NotifyCollectionChangedAction.Add )
				//addingToOtherSide: the item that was just added to this side's collection
				foreach ( object addingToOtherSide in e.NewItems ) {
					System.Console.WriteLine( "new item added to set" );
					ICollection<T> otherSidesCollection = GetOtherSidesCollection( addingToOtherSide );
					if ( ReflectionUtil.IsInitialized(otherSidesCollection) && !otherSidesCollection.Contains(thisSide) ) {
						if ( otherSidesCollection is IList && otherSidesCollection.Contains(thisSide) ) // true if add will cause duplicates
							throw new InvalidOperationException( "Collection is non-unique" );
						otherSidesCollection.Add( thisSide );
					}
				}
			else if ( e.Action == NotifyCollectionChangedAction.Remove )
				//removingFromOtherSide: the item that was just removed from this side's collection
				foreach ( object removingFromOtherSide in e.OldItems ) {
					System.Console.WriteLine( "old item removed from set" );
					ICollection<T> otherSidesCollection = GetOtherSidesCollection( removingFromOtherSide );
					if ( ReflectionUtil.IsInitialized(otherSidesCollection) && otherSidesCollection.Contains(thisSide) ) {
						otherSidesCollection.Remove( thisSide );
						if ( otherSidesCollection is IList && otherSidesCollection.Contains(thisSide) ) // true if there was more than one
							throw new InvalidOperationException( "Collection is non-unique" );
					}
				}
		}

		private ICollection<T> GetOtherSidesCollection( object otherSide ) {
			if ( otherSideProperty == null )
				otherSideProperty = otherSide.GetType().GetProperty( otherSidePropertyName );
			return (ICollection<T>)otherSideProperty.GetValue( otherSide, null );
		}
	}
}
