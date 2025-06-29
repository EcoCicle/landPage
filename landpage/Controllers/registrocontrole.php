<?php
// Configurações do MySQL
$host = "localhost";
$usuario = "root";
$senha = "*Galiv726";
$banco = "ecocycle";

// Conectar ao banco
$conexao = new mysqli($host, $usuario, $senha, $banco);
if ($conexao->connect_error) {
    die("Erro de conexão: " . $conexao->connect_error);
}

// Receber dados do formulário
$tipo = $_POST['tipo']; // 'consumidor' ou 'vendedor'
$email = $_POST['email'];
$senha = $_POST['senha'];
$senha_cripto = password_hash($senha, PASSWORD_DEFAULT); // Criptografar

// Validar confirmação de senha (se necessário)
// if ($_POST['senha'] !== $_POST['confirmar_senha']) { ... }

// Inserir no banco de dados
if ($tipo === "consumidor") {
    $sql = "INSERT INTO consumidores (email, senha) VALUES (?, ?)";
} elseif ($tipo === "vendedor") {
    $sql = "INSERT INTO vendedores (email, senha) VALUES (?, ?)";
} else {
    die("Tipo de usuário inválido.");
}

// Usar Prepared Statements para evitar SQL Injection
$stmt = $conexao->prepare($sql);
$stmt->bind_param("ss", $email, $senha_cripto);

if ($stmt->execute()) {
    echo "Cadastro realizado com sucesso!";
} else {
    echo "Erro: " . $stmt->error;
}

$stmt->close();
$conexao->close();
?>
