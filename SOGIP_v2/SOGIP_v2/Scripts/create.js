
    function campo() {
        $('#SelectedSport ,#SelectedCategory').change(function () { //the event here is change

            concatenated_selec_name = 'Seleccion ' + $('#SelectedCategory option:selected').text() + ' de ' + $('#SelectedSport option:selected').text();

            $('#sele_n').val(concatenated_selec_name); //change the value into the input
        });
}

$(document).ready(function () {
    campo();
});