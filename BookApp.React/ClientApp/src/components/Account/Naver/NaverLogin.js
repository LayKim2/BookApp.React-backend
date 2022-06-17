const NaverLogin = () => {
    const naverLogin = new naver.LoginWithNaverId(
        {
            clientId: "h86b4Fzjf7JgAybYOqo7",
            callbackUrl: "http://127.0.0.1:5500/CodeStore/oldNaverLogin.html",
            loginButton: { color: "green", type: 2, height: 40 }
        }
    );

    naverLogin.init();
}

export default NaverLogin;