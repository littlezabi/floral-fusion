const cartCount = document.querySelector("#product-cart-count");
const cartPrice = document.querySelectorAll(".cart-price-total");

const deleteUser = (userEmail) => {
  $.ajax({
    method: "GET",
    url: `/admin/DeleteUser?userEmail=${userEmail}`,
    success: (e) => {
      const element = document.getElementById(`list-item-${userEmail}`);
      element.remove();
    },
  });
};
const updateCartCount = (increment = 1) => {
  let count = parseInt(cartCount.textContent);
  count = count + increment;
  cartCount.textContent = count;
};
const deleteProduct = (id) => {
  $.ajax({
    method: "GET",
    url: `/admin/DeleteProduct?itemId=${id}`,
    success: (e) => {
      const element = document.getElementById(`list-item-${id}`);
      element.remove();
    },
  });
};
const handleCartRemove = (id, price) => {
  $.ajax({
    method: "GET",
    url: `/Cart/Remove?ItemId=${id}`,
    success: (e) => {
      if (e === "success") {
        const element = document.querySelector(`#cart-item-${id}`);
        cartPrice.forEach((element) => {
          let x = element.textContent.replace("$", "");
          x = (parseInt(x) - price).toFixed(2);
          element.textContent = `${x >= 0 ? x : 0}$`;
        });
        element.remove();
        updateCartCount(-1);
      }
      if (e === "notFound") {
        alert("item not found!");
      }
    },
    error: (e) => console.error(e),
  });
};

const addItemToCart = (Id) => {
  $.ajax({
    method: "GET",
    url: `/Cart/Add?ItemId=${Id}`,
    success: (e) => {
      const message = document.querySelector(`#product-${Id} p`);
      const button = document.querySelector(`#product-${Id} button`);
      if (e === "Success") {
        message.innerHTML = "Product Added to Cart!";
        button.innerHTML = "Added";
        updateCartCount(1);
      } else if (e === "UserNotLogged") {
        message.innerHTML = "User Not Logged Please login!";
        message.classList.add("danger");
      } else if (e === "ProductAlreadyExist") {
        message.innerHTML = "Product already exist in cart!";
        message.classList.add("alert");
      }
      console.log(e);
    },
    error: (e) => {
      console.error(e);
    },
  });
};
