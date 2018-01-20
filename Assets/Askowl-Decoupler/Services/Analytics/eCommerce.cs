using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decoupled.Analytics {

  public class eCommerce : Decoupled.Service<eCommerce> {
    
    public virtual void AddPaymentInfo() {
      Debug.Log("$$$$ Add Payment Info");
    }

    public virtual void AddToCart(
      string id, string name, string category, object quantity,
      string location = null, object price = null, string currency = null, object value = null,
      string origin = null, string destination = null, string startDate = null, string endDate = null) {
      Debug.Log("$$$$ Add To Cart - " + name);
    }

    public virtual void AddToWishlist(
      string id, string name, string category, object quantity,
      string location = null, object price = null, string currency = null, object value = null) {
      Debug.Log("$$$$ Add To Wishlist - " + name);
    }

    public virtual void BeginCheckout(
      object value = null, string currency = null, string transactionId = null,
      string startDate = null, string endDate = null,
      object numberOfNights = null, object numberOfRooms = null, object numberOfPassengers = null,
      string origin = null, string destination = null, string travelClass = null) {
      Debug.Log("$$$$ Begin Checkout for " + transactionId);
    }

    public virtual void CampaignDetails(
      string source = null, string medium = null, string campaign = null,
      string term = null, string contentType = null, string adNetworkClickId = null, string cp1 = null) {
      Debug.Log("$$$$ Campaign Details for " + campaign);
    }

    public virtual void CheckoutProgress(object step, string option = null) {
      Debug.Log("$$$$ Checkout Progress step " + step);
    }

    public virtual void Purchase(
      object value = null, string currency = null, string transactionId = null,
      string startDate = null, string endDate = null,
      object numberOfNights = null, object numberOfRooms = null, object numberOfPassengers = null,
      string origin = null, string destination = null, string travelClass = null) {
      Debug.Log("$$$$ Purchase " + transactionId);
    }

    public virtual void GenerateLead(object value, string currency) {
      Debug.Log("$$$$ Generate Lead");
    }

    public virtual void PresentOffer(
      string itemId, string itemName, string category, object quantity,
      string locationId = null, object price = null, string currency = null, object value = null
    ) {
      Debug.Log("$$$$ Present Offer for " + itemName);
    }

    public virtual void PurchaseRefund(object value = null, string currency = null, string transactionId = null) {
      Debug.Log("$$$$ Purchase Refund for " + transactionId);
    }

    public virtual void RemoveFromCart(
      string id, string name, string category, object quantity,
      string location = null, object price = null, string currency = null, object value = null,
      string origin = null, string destination = null, string startDate = null, string endDate = null) {
      Debug.Log("$$$$ Remove From Cart '" + name + "'");
    }

    public virtual void Search(
      string searchTerm, 
      string startDate = null, string endDate = null, object numberOfNights = null, object numberOfRooms = null,
      object numberOfPassengers = null, string origin = null, string destination = null, string travelClass = null) {
      Debug.Log("$$$$ Search '" + searchTerm + "'");
    }

    public virtual void SetCheckoutOption(int step, string option) {
      Debug.Log("$$$$ Set Checkout Option step " + step + ", option " + option);
    }

    public virtual void ViewItem(
      string id, string name, string category,
      object quantity = null, string location = null, object price = null, string currency = null,
      object value = null, string origin = null, string destination = null, string startDate = null,
      string endDate = null, string flightNumber = null, object numberOfNights = null, object numberOfRooms = null,
      object numberOfPassengers = null, string travelClass = null, string searchTerm = null 
    ) {
      Debug.Log("$$$$ View Item '" + name + "'");
    }

    public virtual void ViewItemList(string category) {
      Debug.Log("$$$$ View Item List '" + category + "'");
    }
  }
}