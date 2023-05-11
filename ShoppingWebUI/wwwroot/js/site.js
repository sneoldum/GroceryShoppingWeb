// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//importScripts()
//const axios = require('axios');
//async function getPro() {

//    // Make a request for a user with a given ID
//    axios.get('~/api/product~/getlist')
//        .then(function (response) {
//            // handle success
//            console.log(response);
//        })
//        .catch(function (error) {
//            // handle error
//            console.log(error);
//        })
//        .then(function () {
//            // always executed
//        });

//}

//document.getElementById("getProduct").innerHTML = getPro();
$('#myModal').on('shown.bs.modal',
    function () {
        $('#myInput').trigger('focus')
    });
document.getElementById("year").innerHTML = new Date().getFullYear();