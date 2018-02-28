using UnityEngine;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global

namespace Decoupled.Analytics {
  using JetBrains.Annotations;

  // ReSharper disable once InconsistentNaming
  public sealed class eCommerceLog : Service<eCommerceLog> {
    [UsedImplicitly]
    public void AddPaymentInfo() { Debug.Log(message: "$$$$ Add Payment Info"); }

    public void AddToCart(
      string             id,                 string             name, string category,
      object             quantity,           [CanBeNull] string location  = null,
      [CanBeNull] object price       = null, [CanBeNull] string currency  = null,
      [CanBeNull] object value       = null, [CanBeNull] string origin    = null,
      [CanBeNull] string destination = null, [CanBeNull] string startDate = null,
      [CanBeNull] string endDate     = null) {
      Debug.Log(message: "$$$$ Add To Cart - " + name);
    }

    public void AddToWishlist(
      string             id, string name, string category, object quantity,
      [CanBeNull] string location = null,
      [CanBeNull] object price    = null, [CanBeNull] string currency = null,
      [CanBeNull] object value    = null) {
      Debug.Log(message: "$$$$ Add To Wishlist - " + name);
    }

    public void BeginCheckout(
      [CanBeNull] object value              = null, [CanBeNull] string currency = null,
      [CanBeNull] string transactionId      = null,
      [CanBeNull] string startDate          = null, [CanBeNull] string endDate       = null,
      [CanBeNull] object numberOfNights     = null, [CanBeNull] object numberOfRooms = null,
      [CanBeNull] object numberOfPassengers = null,
      [CanBeNull] string origin             = null, [CanBeNull] string destination = null,
      [CanBeNull] string travelClass        = null) {
      Debug.Log(message: "$$$$ Begin Checkout for " + transactionId);
    }

    public void CampaignDetails(
      [CanBeNull] string source           = null, [CanBeNull] string medium = null,
      [CanBeNull] string campaign         = null,
      [CanBeNull] string term             = null, [CanBeNull] string contentType = null,
      [CanBeNull] string adNetworkClickId = null, [CanBeNull] string cp1         = null) {
      Debug.Log(message: "$$$$ Campaign Details for " + campaign);
    }

    public void CheckoutProgress(object step, [CanBeNull] string option = null) {
      Debug.Log(message: "$$$$ Checkout Progress step " + step);
    }

    public void Purchase(
      [CanBeNull] object value              = null, [CanBeNull] string currency = null,
      [CanBeNull] string transactionId      = null,
      [CanBeNull] string startDate          = null, [CanBeNull] string endDate       = null,
      [CanBeNull] object numberOfNights     = null, [CanBeNull] object numberOfRooms = null,
      [CanBeNull] object numberOfPassengers = null,
      [CanBeNull] string origin             = null, [CanBeNull] string destination = null,
      [CanBeNull] string travelClass        = null) {
      Debug.Log(message: "$$$$ Purchase " + transactionId);
    }

    public void GenerateLead(object value, string currency) {
      Debug.Log(message: "$$$$ Generate Lead");
    }

    public void PresentOffer(
      string             itemId, string itemName, string category,
      object             quantity,
      [CanBeNull] string locationId = null, [CanBeNull] object price = null,
      [CanBeNull] string currency   = null, [CanBeNull] object value = null
    ) {
      Debug.Log(message: "$$$$ Present Offer for " + itemName);
    }

    public void PurchaseRefund([CanBeNull] object value         = null,
                               [CanBeNull] string currency      = null,
                               [CanBeNull] string transactionId = null) {
      Debug.Log(message: "$$$$ Purchase Refund for " + transactionId);
    }

    public void RemoveFromCart(
      string             id, string name, string category,
      object             quantity,
      [CanBeNull] string location  = null, [CanBeNull] object price       = null,
      [CanBeNull] string currency  = null, [CanBeNull] object value       = null,
      [CanBeNull] string origin    = null, [CanBeNull] string destination = null,
      [CanBeNull] string startDate = null, [CanBeNull] string endDate     = null) {
      Debug.Log(message: "$$$$ Remove From Cart '" + name + "'");
    }

    public void Search(
      string             searchTerm,
      [CanBeNull] string startDate          = null, [CanBeNull] string endDate       = null,
      [CanBeNull] object numberOfNights     = null, [CanBeNull] object numberOfRooms = null,
      [CanBeNull] object numberOfPassengers = null, [CanBeNull] string origin        = null,
      [CanBeNull] string destination        = null, [CanBeNull] string travelClass   = null) {
      Debug.Log(message: "$$$$ Search '" + searchTerm + "'");
    }

    public void SetCheckoutOption(int step, string option) {
      Debug.Log(message: "$$$$ Set Checkout Option step " + step + ", option " + option);
    }

    [UsedImplicitly]
    // ReSharper disable once FunctionComplexityOverflow
    private void ViewItem(
      string id,                        string name,            string category,
      object quantity           = null, string location = null, object price = null,
      string currency           = null,
      object value              = null, string origin = null, string destination = null,
      string startDate          = null,
      string endDate            = null, string flightNumber = null, object numberOfNights = null,
      object numberOfRooms      = null,
      object numberOfPassengers = null, string travelClass = null, string searchTerm = null
    ) {
      Debug.Log(message: "$$$$ View Item '" + name + "'");
    }

    [UsedImplicitly]
    public void ViewItemList(string category) {
      Debug.Log(message: "$$$$ View Item List '" + category + "'");
    }
  }
}