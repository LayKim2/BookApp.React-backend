import React, { useState, useEffect } from "react";
import axios from 'axios';
import { NaverLogin } from "./NaverLogin";
import {
    useLocation, Link
} from "react-router-dom";


export function Login2() {



    const { naver } = window;
    const location = useLocation();
    const NAVER_CALLBACK_URL = 'https://localhost:44406/login2';
    const NAVER_CLIENT_ID = 'h86b4Fzjf7JgAybYOqo7';

    const initializeNaverLogin = () => {

        const naverLogin = new naver.LoginWithNaverId({
            clientId: NAVER_CLIENT_ID,
            callbackUrl: NAVER_CALLBACK_URL,
            isPopup: true,
            loginButton: { color: "green", type: 3, height: 60 },
        });
        naverLogin.init();
        naverLogin.getLoginStatus(function (status) {
            alert(status);
            if (status) {
                alert('here1');
                const nickName = naverLogin.user.getNickName();
                const age = naverLogin.user.getAge();
                const birthday = naverLogin.user.getBirthday();

                if (nickName === null || nickName === undefined) {
                    alert("별명이 필요합니다. 정보제공을 동의해주세요.");
                    naverLogin.reprompt();
                    return;
                } else {
                    alert('here2');
                    setLoginStatus();
                }
            }
        });
        console.log(naverLogin);

        //const getNaverToken = () => {
        //    if (!location.hash) return;
        //    const token = location.hash.split('=')[1].split('&')[0]; //token 출력
        //    axios.post(`${process.env.REACT_APP_SERVER_API}/user/naver-login`, {
        //        token
        //    }, {
        //        withCredentials: true
        //    })
        //        .then((res) => {
        //            window.location.replace('/')
        //            //서버측에서 로직이 완료되면 홈으로 보내준다
        //        })
        //};

        function setLoginStatus() {

            const message_area = document.getElementById('message');
            message_area.innerHTML = `
              <h3> Login 성공 </h3>
              <div>user Id : ${naverLogin.user.id}</div>
              <div>user Nickname : ${naverLogin.user.nickname}</div>
              <div>user Age(범위) : ${naverLogin.user.age}</div>
              <div>user Birthday : ${naverLogin.user.birthday}</div>
              `;

            const button_area = document.getElementById('button_area');
            button_area.innerHTML = "<button id='btn_logout'>로그아웃</button>";

            const logout = document.getElementById('btn_logout');
            logout.addEventListener('click', (e) => {
                naverLogin.logout();
                //location.replace("https://localhost:44406");
            })
        }
    };

    const getNaverToken = () => {
        alert('here333');
        if (!location.hash) {
            alert('null');
            return;
        }
        else {
            alert('yes');
            const token = location.hash.split('=')[1].split('&')[0]; //token 출력

            const userData = axios.get('https://openapi.naver.com/v1/nid/me', {
                headers: {
                    'Authorization': `Bearer ${token}`,
                }
                //axios.post(`${process.env.REACT_APP_SERVER_API}/user/naver-login`, {
                //    token
                //}, {
                //    withCredentials: true
                //})
                //    .then((res) => {
                //        alert(res);
                //        window.location.replace('/');
                //        //서버측에서 로직이 완료되면 홈으로 보내준다
                //    })

            })
        }


    };

    useEffect(() => {
        getNaverToken();
        initializeNaverLogin();
    }, []);


    return (
        <>
            <div className="row">
                <div className="col-md-8">
                    <form>
                        <div className="form-group">
                            <div class="login-area">
                                <div id="message">
                                    로그인 버튼을 눌러 로그인 해주세요.
                                </div>
                                <div id="button_area">
                                    <div className="grid-naver" id='naverIdLogin'></div>
                                </div>

{/*                                <Link to="javascript:kakaoLogin();"><img style="" src="https://www.gb.go.kr/Main/Images/ko/member/certi_kakao_login.png" /></Link>*/}
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </>
    );
}