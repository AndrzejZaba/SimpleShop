window.redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51PNYRdFOYkzg0jLIeZg782PCgHw6AwtiGwmN4Ht4OFgSBYQyJ0yDacTkiylZqSZYZbfBV3SVtFnRS9bywHGZCtz000tUp6mIsw");
    stripe.redirectToCheckout({sessionId: sessionId})
}