
    function campo() {
        $('#SelectedSport ,#SelectedCategory').change(function () { //the event here is change

            concatenated_selec_name = $('#SelectedCategory option:selected').text();
            concatenated_selec_mane = $('#SelectedSport option:selected').text();

            $('#sele_n').val(concatenated_selec_name); //change the value into the input
            $('#sele_m').val(concatenated_selec_mane); //change the value into the input
        });
}


$(document).ready(function () {
    campo();
});