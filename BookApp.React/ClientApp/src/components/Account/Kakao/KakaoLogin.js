import React from 'react';
import Auth from "./Auth";
import Profile from "./Profile";
import { Route } from 'react-router';
import { BrowserRouter as Router, Switch } from "react-router-dom";

const KakaoLogin = () => {

    const REST_API_KEY = "e29ac0a82bef45de6694baff7e5aa83e";
    const REDIRECT_URI = "https://localhost:44406/KakaoLogin/Callback";
    const KAKAO_AUTH_URL = `https://kauth.kakao.com/oauth/authorize?client_id=${REST_API_KEY}&redirect_uri=${REDIRECT_URI}&response_type=code`;

    return (
        <>
            <a href={KAKAO_AUTH_URL}><img width="200px" src="https://www.gb.go.kr/Main/Images/ko/member/certi_kakao_login.png" /></a>
        </>
    )
}

export default KakaoLogin;
