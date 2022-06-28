let left = document.querySelectorAll(".fa-chevron-left");
let right = document.querySelectorAll(".fa-chevron-right");
let slider = document.querySelector(".slider");
let header = document.querySelector(".navbar-section");




$(document).ready(function () {
    $(".all-category").show();
    $(".cards-category").hide();
    $(".app-category").hide();
    $(".web-category").hide();
})

$(document).ready(function () {
    $(".app").click(function () {
        $(".app-category").show();
        $(".cards-category").hide();
        $(".web-category").hide();
        $(".all-category").hide();
    })
})
$(document).ready(function() {
     $(".cards").click(function(){
        $(".app-category").hide();
        $(".cards-category").show();
        $(".web-category").hide();
        $(".all-category").hide();
     })
})
$(document).ready(function(){
    $(".web").click(function(){
        $(".app-category").hide();
        $(".cards-category").hide();
        $(".web-category").show();
        $(".all-category").hide();
     })
})
$(document).ready(function(){
    $(".all").click(function(){
        $(".app-category").hide();
        $(".cards-category").hide();
        $(".web-category").hide();
        $(".all-category").show();
    })
})
$('.owl-carousel').owlCarousel({
    loop:true,
    margin:10,
    nav:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:3
        },
        1000:{
            items:5
        }
    }
})

right.forEach(rights => {
    rights.addEventListener("click", function () {

        let active = document.querySelector(".actives");
        active.classList.remove("actives");


        if (active.nextElementSibling == null) {
            slider.firstElementChild.classList.add("actives");
        }
        else {
            active.nextElementSibling.classList.add("actives");
        }
    })
})

left.forEach(lefts => {
    lefts.addEventListener("click", function () {
        let active = document.querySelector(".actives");
        active.classList.remove("actives");


        if (active.previousElementSibling == null) {
            slider.lastElementChild.classList.add("actives");
        }
        else {
            active.previousElementSibling.classList.add("actives");
        }
    })
})


