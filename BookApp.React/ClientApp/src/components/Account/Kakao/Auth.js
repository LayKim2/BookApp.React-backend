import React from "react";
import { useEffect } from "react";
import axios from "axios";
import qs from "qs";
import { useHistory } from "react-router-dom";

const Auth = () => {
    const REST_API_KEY = "e29ac0a82bef45de6694baff7e5aa83e";
    const REDIRECT_URI = "https://localhost:44406/KakaoLogin/Callback";
    const CLIENT_SECRET = "IW5pdOr5XyoyqA2weAPaIM5IqplszFg6";
    // calllback으로 받은 인가코드
    const code = new URL(window.location.href).searchParams.get("code");
    const history = useHistory();
    const getToken = async () => {
        const payload = qs.stringify({
            grant_type: "authorization_code",
            client_id: REST_API_KEY,
            redirect_uri: REDIRECT_URI,
            code: code,
            client_secret: CLIENT_SECRET,
        });
        try {
            // access token 가져오기
            const res = await axios.post(
                "https://kauth.kakao.com/oauth/token",
                payload
            );

            // Kakao Javascript SDK 초기화
            window.Kakao.init(REST_API_KEY);
            // access token 설정
            window.Kakao.Auth.setAccessToken(res.data.access_token);
            history.replace("/profile");
        } catch (err) {
            console.log(err);
        }
    };
    useEffect(() => {
        getToken();
    }, []);
    return null;
};
export default Auth;