$(document).ready(function () {

    // lista de dados que aparecem na tela , exceto os favoritos
    var dados_tela =[];
    //requisição obter Repositórios de um usuario do Github  
 
    $('#myreps').click(function () {

        $.ajax({
            method: 'GET',
            dataType: "json",
            contentType: 'application/json',
            url: 'https://api.github.com/users/' + $('#usuario').val() + '/repos',
            success: function (response, s) { atualizarTabela(response); },
            error: function (request, status, error) { alert(error); }
        });
    });

    // requisição para repositorios com um nome
    $('#allreps').click(function () {

        $.ajax({
            method: 'GET',
            dataType: "json",
            contentType: 'application/json',
            url: 'https://api.github.com/search/repositories?q=' + $('#buscar_rep').val(),
            success: function (response, s) { atualizarTabelaSearch(response); },
            error: function (request, status, error) { alert(error); }
        });

    });

    
    //preencher a tabela de repositorios de um usuario
    function atualizarTabela(response) {
        $('#reps tr').remove();

        for (i = 0; i < response.length; i++){

            var e = $('#reps').append('<tr><td id="' + response[i].id + '">' + response[i].name + '</td></tr>');
            dados_tela.push({ ID: response[i].id, Nome: response[i].name, Descricao: response[i].description, UltimaData: response[i].updated_at, CriadorReposit: response[i].owner.login, Linguagem: response[i].language});
        }
        
    }
    // abrir nova tela ao clicar em um repositório
    $('.data').click(function (e) {

        for (i = 0; i < dados_tela.length; i++) {
            if (dados_tela[i].ID == e.target.id) {

                $.ajax({
                    method: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: string,
                    data: JSON.stringify(dados_tela[i]),
                    success: function (response) {
                        window.location.href = response.urlT;
                    }
                });
            }
        }

        
    });
    //preencher a tabela de repositorios procurados
    function atualizarTabelaSearch(response) {
        $('#search_reps tr').remove();

        for (i = 0; i < response.items.length; i++) {
            $('#search_reps').append('<tr><td id="'+response.items[i].id+'">' + response.items[i].name + '</td></tr>');
            dados_tela.push({ ID: response.items[i].id, Nome: response.items[i].name, Descricao: response.items[i].description, UltimaData: response.items[i].updated_at, CriadorReposit: response.items[i].owner.login, Linguagem: response.items[i].language });

        }

    }

    //Preencher a tabela de de favoritos na pagina inicial
    for (i = 0; i < teste.length; i++) {

        $('#fav_reps').append('<tr>' +
            '<td>' + teste[i].Nome + '</td>'+
            '<td> ' + teste[i].Descricao+' ' + '</td>'+
            '<td>' + teste[i].CriadorReposit + '</td>'+
            '<td>' + teste[i].UltimaData + '</td></tr>');
    }

});

