function toggleLogin(isLogin) {
    document.getElementById('btn-login').classList.toggle('active', isLogin);
    document.getElementById('btn-registro').classList.toggle('active', !isLogin);

    document.getElementById('form-login').style.display = isLogin ? 'block' : 'none';
    document.getElementById('botoes-registro').style.display = isLogin ? 'none' : 'block';

    document.getElementById('form-consumidor').style.display = 'none';
    document.getElementById('form-vendedor').style.display = 'none';
    document.getElementById('form-loja').style.display = 'none';
  }
function mostrarFormLogin() {
    document.getElementById('botoes-iniciais').style.display = 'none';
    document.getElementById('form-login').style.display = 'block';
    document.getElementById('botoes-registro').style.display = 'none';
    document.getElementById('form-consumidor').style.display = 'none';
    document.getElementById('form-vendedor').style.display = 'none';
    document.getElementById('form-loja').style.display = 'none';
}

function mostrarRegistro() {
    document.getElementById('botoes-iniciais').style.display = 'none';
    document.getElementById('form-login').style.display = 'none';
    document.getElementById('botoes-registro').style.display = 'block';
    document.getElementById('form-consumidor').style.display = 'none';
    document.getElementById('form-vendedor').style.display = 'none';
    document.getElementById('form-loja').style.display = 'none';
}

function mostrarFormConsumidor() {
    document.getElementById('botoes-registro').style.display = 'none';
    document.getElementById('form-consumidor').style.display = 'block';
    document.getElementById('form-vendedor').style.display = 'none';
    document.getElementById('form-loja').style.display = 'none';
    document.getElementById('form-login').style.display = 'none';
}

function mostrarFormVendedor() {
    document.getElementById('botoes-registro').style.display = 'none';
    document.getElementById('form-vendedor').style.display = 'block';
    document.getElementById('form-consumidor').style.display = 'none';
    document.getElementById('form-loja').style.display = 'none';
    document.getElementById('form-login').style.display = 'none';
}

function mostrarFormLoja(event) {
    event.preventDefault();
    document.getElementById('form-vendedor').style.display = 'none';
    document.getElementById('form-loja').style.display = 'block';
}

function voltarParaIniciais() {
    document.getElementById('botoes-registro').style.display = 'block';
    document.getElementById('form-login').style.display = 'none';
    document.getElementById('form-consumidor').style.display = 'none';
    document.getElementById('form-vendedor').style.display = 'none';
    document.getElementById('form-loja').style.display = 'none';
}

function voltarParaRegistro() {
    document.getElementById('botoes-registro').style.display = 'block';
    document.getElementById('form-consumidor').style.display = 'none';
    document.getElementById('form-vendedor').style.display = 'none';
    document.getElementById('form-loja').style.display = 'none';
    document.getElementById('form-login').style.display = 'none';
}

function voltarParaFormVendedor() {
    document.getElementById('form-vendedor').style.display = 'block';
    document.getElementById('form-loja').style.display = 'none';
}

function toggleSenha(inputId, btn) {
  const input = document.getElementById(inputId);
  if (input.type === "password") {
    input.type = "text";
    btn.innerText = "🙈";
  } else {
    input.type = "password";
    btn.innerText = "👁️";
  }
}

function enviarFormulario() {
  const form = document.getElementById('form-consumidor');
  const formData = new FormData(form);

  fetch('/LoginRegister/CreateConsumidor', {
    method: 'POST',
    body: formData
  })
    .then(response => response.text())
    .then(data => {
      try {
        const result = JSON.parse(data);
        const errorDiv = document.getElementById('login-error-message');
        if (result.error === true) {
          if (errorDiv) {
            errorDiv.innerText = result.message || 'Erro ao registrar.';
            errorDiv.style.display = 'block';
          } else {
            const div = document.createElement('div');
            div.id = 'login-error-message';
            div.style.textAlign = 'center';
            div.style.color = 'red';
            div.innerText = result.message || 'Erro ao registrar.';
            document.getElementById('form-consumidor').appendChild(div);
          }
        } else {
          window.location.href = '/Home/Configuracao';
        }
      } catch (e) {
        console.error('Erro ao processar resposta:', e);
      }
    })
    .catch(error => {
      console.error('Erro:', error);
    });
}

document.getElementById('form-loja').addEventListener('submit', function(event) {
  event.preventDefault();
  const email = document.getElementById('email-vendedor').value;
  const senha = document.getElementById('senha-vendedor').value;
  const confirmarSenha = document.getElementById('confirmar-senha-vendedor').value;
  const cnpj = document.getElementById('cnpj-vendedor').value;
  const nomeloja = document.getElementById('nome-loja').value;
  const descricaoloja = document.getElementById('descricao-loja').value;

  const formData = new FormData();
  formData.append('emailvendedor', email);
  formData.append('senhavendedor', senha);
  formData.append('confirmarsenhavendedor', confirmarSenha);
  formData.append('cnpjvendedor', cnpj);
  formData.append('nomeloja', nomeloja);
  formData.append('descricaoloja', descricaoloja);

  fetch('/LoginRegister/CreateVendedor', {
    method: 'POST',
    body: formData
  })
  .then(response => response.json())
  .then(data => {
    if (data.error) {
      alert(data.message);
    } else {
      window.location.href = '/Home/Configuracao';
    }
  });
});