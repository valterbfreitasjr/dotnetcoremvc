document.addEventListener("DOMContentLoaded", function () {
    carregarDadosTabela();
});

document.getElementById("btnNovoRegistro").addEventListener("click", function () {
    ModalNovoRegistro();
});

document.getElementById("salvar_registro").addEventListener("click", function () {
    NovoRegistro();
});

document.getElementById("alterar_registro").addEventListener("click", function () {
    AtualizarRegistro();
});

async function carregarDadosTabela() {
    const response = await fetch('pedido/obtertodos');
    const data = await response.json();
    let tr = '';
    data.map(dado => {
        tr += `<tr>
               <td>${dado.id}</td>
               <td>${dado.cliente.nome}</td> 
               <td>${dado.funcionario.nome}</td>
               <td>${converterPadraoDataBr(dado.dataPedido)}</td>
               <td><button class="btn btn-icon btn-primary" id="btnProdutos${dado.id}" onclick="ExibirProdutos(${dado.id})"><i class="fa fa-eye"></i></button></td>
               <td><button class="btn btn-icon btn-primary" id="btnServiços${dado.id}" onclick="ExibirServicos(${dado.id})"><i class="fa fa-eye"></i></button></td>
               <td>${Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(dado.valorTotal)}</td>
               <td>
                    <button class="btn btn-icon btn-danger" id="btnRemover${dado.id}" onclick="Remover(${dado.id})"><i class="fa fa-trash"></i></button>
               </td>
               </tr>`;

    });
    jQuery('#PedidoCadastrado > tbody').html(tr);
    let nomeTabela = 'PedidoCadastrado';
    dataTable(nomeTabela);
    fecharLoader();
}

function converterPadraoDataBr(dt) {
    if (dt != null && dt != "") {
        dt = dt.split('-');
        let t = dt[2].split('T');
        let data = t[0] + '/' + dt[1] + '/' + dt[0];
        return data;
    }
    return "";
}

function ExibirProdutos(id) {

    AtualizaTableProdutos();
    carregarDadosProduto(id);
    jQuery('#modalProdutos').modal({
        backdrop: 'static',
        keyboard: false
    }) 
}

async function carregarDadosProduto(id) {
    const response = await fetch('produto/obterprodutospedido/' + id);
    const data = await response.json();
    let tr = '';
    data.map(dado => {
        tr += `<tr>
               <td>${dado.id}</td>
               <td>${dado.descricao}</td> 
               <td>${Intl.NumberFormat('pt-br', { style: 'currency', currency: 'BRL' }).format(dado.preco)}</td>
               </tr>`;

    });
    jQuery('#TabelaExibirProdutos > tbody').html(tr);
    let nomeTabela = 'TabelaExibirProdutos';
    dataTable(nomeTabela);
    fecharLoader();
}

function AtualizaTableProdutos() {

    document.getElementById('destroyProduto').innerHTML = "";
    document.getElementById('destroyProduto').innerHTML = `
                    <table class="table table-striped" id="TabelaExibirProdutos">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Descrição</th>
                                <th scope="col">Preço</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>    `;
}

function AtualizaTable() {

    document.getElementById('destroy').innerHTML = "";
    document.getElementById('destroy').innerHTML = `
                    <table class="table table-striped" id="ProdutoCadastrado">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Descrição</th>
                                <th scope="col">Preço</th>
                                <th scope="col">Opções</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>    `;

    carregarDadosTabela();
}

