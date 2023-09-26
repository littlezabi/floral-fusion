const addItemToCart = (Id) => {
  $.ajax({
    method: "GET",
    url: `/Cart/Add?ItemId=${Id}`,
    success: (e) => {
      const message = document.querySelector(`#product-${Id} p`)
      const button = document.querySelector(`#product-${Id} button`)
      if(e === "Success"){
        message.innerHTML = "Product Added to Cart!";
        button.innerHTML = "Added";
        const cartCount = document.querySelector("#product-cart-count")
        let count = parseInt(cartCount.textContent);
        count = count + 1
        cartCount.textContent = count;
      }
      else if(e === "UserNotLogged"){
        message.innerHTML = "User Not Logged Please login!";
        message.classList.add("danger")
      }
      else if(e === "ProductAlreadyExist"){
        message.innerHTML = "Product already exist in cart!";
        message.classList.add("alert")
      }
      console.log(e);
    },
    error: (e) => {
      console.error(e);
    },
  });
};
