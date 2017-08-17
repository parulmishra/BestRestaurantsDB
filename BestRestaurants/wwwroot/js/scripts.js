function sortCuisines(descending){
  var cuisineUl = document.getElementsByClassName("cuisine-sort");
  var restaurantUl = document.getElementsByClassName("restaurant-sort");
  var valsCuisine = [];
  var valsrestaurantUl = [];

  for (var i = 0, l = cuisineUl.length; i < l; i++)
  {
    valsCuisine.push(cuisineUl[i].innerHTML);
  }
  valsCuisine.sort();

  for (var i = 0, l = restaurantUl.length; i < l; i++)
  {
    valsrestaurantUl.push(cuisineUl[i].innerHTML);
  }
  valsrestaurantUl.sort();

  if (descending){
    valsCuisine.reverse();
    valsrestaurantUl.reverse();
  }

  for (var i = 0, l = cuisineUl.length; i < l; i++){
    cuisineUl.innerHTML = valsCuisine[i];
  }

  for (var i = 0, l = cuisineUl.length; i < l; i++){
    restaurantUl.innerHTML = valsrestaurantUl[i];
  }
}



window.onload = function() {
  var desc = false;
  console.log('hi');
  document.getElementById("cuisine-name").onclick = function() {
    console.log("hello");
    sortCuisines(desc);
    desc = !desc;
    return false;
  }
}
