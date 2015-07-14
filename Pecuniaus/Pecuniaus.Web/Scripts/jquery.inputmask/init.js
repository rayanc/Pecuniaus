$(document).ready(function () {
    $(".input-phonemask").inputmask("(999)999-9999");  //static mask
    $(".input-currency").inputmask("currency", { prefix: '', placeholder: '' });
});

$(document).ajaxComplete(function () {
    $(".input-phonemask").inputmask("(999)999-9999");  //static mask
    $(".input-currency").inputmask("currency", { prefix: '',  placeholder:''});
});