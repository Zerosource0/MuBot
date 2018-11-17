import axios from "axios";
import { IToken, TokenStoreKey, ProxyLink } from "./SpotifyAuthController";

function getToken(): IToken | void {
    let token!: IToken;
    const tokenStr = localStorage.getItem(TokenStoreKey);
    if (tokenStr != null) {
        token = JSON.parse(tokenStr);
        return token;
    }
}

async function getProfileInfo(): Promise<any> {
    const token = getToken();
    if (!token) {
        return;
    }

    await axios({
        method: "get",
        url: ProxyLink + "https://api.spotify.com/v1/me",
        headers: {
            "Authorization": "Bearer " + token.access_token,
            "Accept": "*",
            "X-Requested-With": "XMLHttpRequest",
            "Access-Control-Allow-Origin": "*",
            // tslint:disable-next-line:max-line-length
            "Access-Control-Allow-Headers": "Access-Control-Allow-Origin,Origin,Referer,User-Agent,GET,POST,OPTIONS,Accept,Authorization,Content-Type,X-Requested-With",
            "Content-Type": "application/x-www-form-urlencoded"
        },
    }).catch((error) => {
        if (error) {
            console.log("error getting token", error);
            return;
        }
    }).then((profileInfo: any) => {
        console.log(profileInfo);
        return profileInfo;
    });
}

export default {
    getProfileInfo,
};
