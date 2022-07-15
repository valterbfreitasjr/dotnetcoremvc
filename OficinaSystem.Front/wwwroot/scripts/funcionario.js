document.addEventListener("DOMContentLoaded", function () {
    carregarDadosTabela();
});

async function carregarDadosTabela() {
    const response = await fetch('funcionario/obtertodos');
    const data = await response.json();
    let tr = '';
    data.map(dado => {
        tr += `<tr>
               <td>${dado.id}</td>
               <td>${dado.nome}</td> 
               <td>${dado.cpf}</td> 
               <td>${dado.endereco}</td> 
               <td>
                    <button class="btn btn-icon btn-primary" id="btnAlterar${dado.id}" onclick="Alterar(${dado.id})"><i class="fa fa-edit"></i></button>
                    <button class="btn btn-icon btn-danger" id="btnRemover${dado.id}" onclick="Remover(${dado.id})"><i class="fa fa-trash"></i></button>
               </td>
               </tr>`;

    });
    jQuery('#FuncionarioCadastrado > tbody').html(tr);
    let nomeTabela = 'FuncionarioCadastrado';
    dataTable(nomeTabela);
    fecharLoader();
}
