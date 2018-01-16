using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decoupled.Analytics {

  public class eCommerce : Decoupled.Service<eCommerce> {
    
    public virtual void AddPaymentInfo() {
    }

    public virtual void AddToCart(
      string id, string name, string category, object quantity,
      string location = null, object price = null, string currency = null, object value = null,
      string origin = null, string destination = null, string startDate = null, string endDate = null) {
    }

    public virtual void AddToWishlist(
      string id, string name, string category, object quantity,
      string location = null, object price = null, string currency = null, object value = null) {
    }

    public virtual void BeginCheckout(
      object value = null, string currency = null, string transactionId = null,
      string startDate = null, string endDate = null,
      object numberOfNights = null, object numberOfRooms = null, object numberOfPassengers = null,
      string origin = null, string destination = null, string travelClass = null) {
    }

    public virtual void CampaignDetails(
      string source = null, string medium = null, string campaign = null,
      string term = null, string contentType = null, string adNetworkClickId = null, string cp1 = null) {
    }

    public virtual void CheckoutProgress(object step, string option = null) {
    }

    public virtual void Purchase(
      object value = null, string currency = null, string transactionId = null,
      string startDate = null, string endDate = null,
      object numberOfNights = null, object numberOfRooms = null, object numberOfPassengers = null,
      string origin = null, string destination = null, string travelClass = null) {
    }

    public virtual void GenerateLead(object value, string currency) {
    }

    public virtual void PresentOffer(
      string itemId, string itemName, string category, object quantity,
      string locationId = null, object price = null, string currency = null, object value = null
    ) {
    }

    public virtual void PurchaseRefund(object value = null, string currency = null, string transactionId = null) {
    }

    public virtual void RemoveFromCart(
      string id, string name, string category, object quantity,
      string location = null, object price = null, string currency = null, object value = null,
      string origin = null, string destination = null, string startDate = null, string endDate = null) {
    }

    public virtual void Search(
      string searchTerm, 
      string startDate = null, string endDate = null, object numberOfNights = null, object numberOfRooms = null,
      object numberOfPassengers = null, string origin = null, string destination = null, string travelClass = null) {
    }

    public virtual void SetCheckoutOption(int step, string option) {
    }

    public virtual void ViewItem(
      string id, string name, string category,
      object quantity = null, string location = null, object price = null, string currency = null,
      object value = null, string origin = null, string destination = null, string startDate = null,
      string endDate = null, string flightNumber = null, object numberOfNights = null, object numberOfRooms = null,
      object numberOfPassengers = null, string travelClass = null, string searchTerm = null 
    ) {
    }

    public virtual void ViewItemList(string category) {
    }
  }
}