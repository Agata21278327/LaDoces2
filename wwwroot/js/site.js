const cep = document.querySelector("#cep");
const rua = document.querySelector('#rua');
const bairro = document.querySelector('#bairro');
const cidade = document.querySelector('#cidade');
const estado = document.querySelector('#estado');

cep.addEventListener('blur', e=> {
  consultaCep();
});

async function consultaCep(){

          rua.value = ""
          bairro.value = ""
          cidade.value = ""
          estado.value = ""
 
  const value = cep.value;

  const url = `https://viacep.com.br/ws/${value}/json/`;

  await fetch(url)
  .then(response => response.json())
  .then( json => {
       
          rua.value = json.logradouro;
          bairro.value = json.bairro;
          cidade.value = json.localidade;
          estado.value = json.uf;
       
    })
    .catch( e => {
      alert("Cep Errado! ou Falha no Servidor!")
    })
   
  }