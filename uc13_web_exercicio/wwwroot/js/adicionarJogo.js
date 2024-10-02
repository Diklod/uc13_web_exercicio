$(document).ready(function () {

    $('#frmJogo').on('submit', function (e) {
        CadastrarJogo(e);
    });

    $('#frmJogo').on('click', '.btnDelete', function (e) {
        Delete(e);
    });

});

function CadastrarJogo(e) {
    
    e.preventDefault();

    var operation = $('#btnSave').attr('operation');
    var itemId = $('#btnSave').attr('itemId');
    var commandUrl = "";

    if (operation == "add")
        commandUrl = '/Home/CadastrarJogo';
    else if (operation = "upd")
        commandUrl = '/Home/EditarJogo';

    if ($('#selectMultiplayer').val() == "1") {
        var selecaoMultiplayer = true;
    } else {
        var selecaoMultiplayer = false;
    }

    var formData = {
        id: parseInt(itemId),
        Nome: $('#txtNome').val(),
        Genero: $('#txtGenero').val(),
        Classificacao: $('#txtClassificacao').val(),
        Idiomas: $('#txtIdiomas').val(),
        Preco: parseFloat($('#numPreco').val().replace('R$', '').replace('.', '').replace(',', '.')),
        Multiplayer: selecaoMultiplayer,
        ConfigMinimina: $('#txtConfigMin').val(),
        ConfigRecomendada: $('#txtConfigRecomendada').val(),
        Resumo: $('#txtResumo').val()
    }
    $.ajax({
        url: commandUrl,
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',

        success: function (data) {

            if (data.success) {

                Swal.fire({
                    html: data.message,
                    icon: "success",
                    confirmButtonText: "Sim",
                    timer: 2000,
                    customClass: {
                        confirmButton: "btn btn-primary",
                        cancelButton: "btn btn-secondary"
                    }
                }).then(function (result) {

                    if (result.value) {

                        window.location.href = '/Home/JogosCadastrados';
                    }
                });

            }
        }
    });
}

function mascaraMoeda(input) {

    let valor = input.value.replace(/[^0-9]/g, '');

    valor = (valor / 100).toFixed(2).replace('.', ',');
    valor = valor.replace(/\B(?=(\d{3})+(?!\d))/g, '.');

    input.value = 'R$' + valor;
}