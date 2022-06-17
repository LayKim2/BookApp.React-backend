import React, { useEffect, useState } from "react";

const Profile = () => {
    const [user_id, setUserId] = useState();
    const [user_email, setUserEmail] = useState();
    const [nickName, setNickName] = useState();
    const [profileImage, setProfileImage] = useState();
    const getProfile = async () => {
        try {
            // Kakao SDK API를 이용해 사용자 정보 획득
            let data = await window.Kakao.API.request({
                url: "/v2/user/me",
            });
            // 사용자 정보 변수에 저장
            console.log(data);
            setUserId(data.id);
            setUserEmail(data.kakao_account.email);
            setNickName(data.kakao_account.profile.nickname);
            setProfileImage(data.kakao_account.profile.thumbnail_image_url);
        } catch (err) {
            console.log(err);
        }
    };
    useEffect(() => {
        getProfile();
    }, []);
    return (
        <div>
            <h2>{user_id}</h2>
            <h2>{user_email}</h2>
            <h2>{nickName}</h2>
            <img src={profileImage}></img>
        </div>
    );
};
export default Profile;