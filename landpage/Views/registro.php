<form action="../controller/RegistroController.php" method="POST">
    <input type="hidden" name="tipo" value="consumidor">
    <input type="email" name="email" placeholder="E-mail" required>
    <input type="password" name="senha" placeholder="Senha" required>
    <button type="submit">Cadastrar</button>
    <?php if (isset($_GET['erro'])): ?>
        <div class="erro"><?php echo htmlspecialchars($_GET['erro']); ?></div>
    <?php endif; ?>
</form>
