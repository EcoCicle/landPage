function toggleLogin(isLogin) {
    document.getElementById('btn-login').classList.toggle('active',!isLogin);
    document.getElementById('btn-registro').classList.toggle('active', isLogin);

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