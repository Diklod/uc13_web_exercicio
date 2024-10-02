$(document).ready(function () {

    LoadGames();

    $('#tbGames').on('click', '.btnEdit', function (e) {
        EditGame(e);
    });

    $('#tbGames').on('click', '.btnDelete', function (e) {
        Delete(e);
    });

});

function LoadGames() {

    new DataTable('#tbGames', {

        ajax: {
            url: '/Home/ListarJogos',
            type: 'GET'
        },

        columns: [
            { data: 'id' },
            { data: 'nome' },
            { data: 'genero' },
            { data: 'classificacao' },
            { data: 'idiomas' },
            { data: 'preco' },
            { data: 'multiplayer' },
            { data: 'configMinimina' },
            { data: 'configRecomendada' },
            { data: 'resumo' },
            {

                //Identifica o delete como classe porque são vários botões
                data: '',
                render: (data, type, row) => {
                    return '<div style="width:100%; text-align:right;">\
                                <a href = "/Home/Edit?id=' + row.id + '" class="btn btn-primary" > Editar</a >\
                                <button type="submit" itemId="' + row.id + '" class="btn btn-danger btnDelete">Excluir</button>\
                           </div>';
                }
            }
        ],

        ordering: true,
        paging: true,

        language: {
            lengthMenu: "Mostrando _MENU_ registros",
            zeroRecords: "Nenhum registro encontrado",
            info: "Página _PAGE_ de _PAGES_",
            infoEmpty: "Nenhum registro encontrado",
            infoFiltered: "(filtrando de _MAX_ registros)",
            loadingRecords: "Carregando...",
            search: "Pesquisa:",

            paginate: {
                first: "Primeiro",
                last: "Último",
                next: "Próximo",
                previous: "Anterior"
            },
        }
    });
}

function Delete(e) {

    var id = $(e.target).attr('itemId');


    Swal.fire({
        title: "Excluir Jogo",
        html: "Confirma a exclusão do jogo?",
        icon: "question",
        confirmButtonText: "Sim",
        showCancelButton: true,
        cancelButtonText: "Não",
        customClass: {
            confirmButton: "btn btn-primary",
            cancelButton: "btn btn-secondary"
        }
    }).then(function (result) {

        if (result.value) {

            $.ajax({
                url: '/Home/Delete?id=' + id,
                type: 'DELETE',
                data: JSON.stringify(id),
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (data) {

                    if (data.success) {

                        Swal.fire({
                            html: 'Jogo excluído com sucesso',
                            icon: 'success',
                            timer: 3000,
                            timerProgressBar: true,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(function (result) {
                            window.location.href = '/Home/JogosCadastrados';
                        });
                    }
                }
            });
        }
    });
}