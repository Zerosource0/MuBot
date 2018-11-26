import axios from "axios";
import { IToken, TokenStoreKey, ProxyLink } from "./SpotifyAuthController";
import endpointService from "../api/endpoint.service";

interface IImage {
    height: any;
    url: string;
    width: any;
}

export interface IProfileInfo {
    display_name: string;
    external_urls: any;
    followers: {
        href: any,
        total: number
    };
    href: string;
    images: IImage[];
    type: string;
    uri: string;
}


function getToken(): IToken | void {
    let token!: IToken;
    const tokenStr = localStorage.getItem(TokenStoreKey);
    if (tokenStr != null) {
        token = JSON.parse(tokenStr);
        return token;
    }
}

async function getProfileInfo(): Promise<IProfileInfo | undefined> {
    const token = getToken();
    if (!token) {
        return;
    }

    const axiosInstance = axios.create({
        headers: {
            "Authorization": "Bearer " + token.access_token,
            "Accept": "*",
            "X-Requested-With": "XMLHttpRequest",
            "Access-Control-Allow-Origin": "*",
            // tslint:disable-next-line:max-line-length
            "Access-Control-Allow-Headers": "Access-Control-Allow-Origin,Origin,Referer,User-Agent,GET,POST,OPTIONS,Accept,Authorization,Content-Type,X-Requested-With",
            "Content-Type": "application/x-www-form-urlencoded"
        },
    });
    const url = ProxyLink + "https://api.spotify.com/v1/me";

    const profileInfo = endpointService.get<IProfileInfo>(url, undefined, axiosInstance);
    console.log(profileInfo);
}

export default {
    getProfileInfo,
};
