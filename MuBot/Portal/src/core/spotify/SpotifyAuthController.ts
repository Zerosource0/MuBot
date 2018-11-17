import * as oauth2 from "simple-oauth2";
import { Md5 } from "ts-md5/dist/md5";
import axios from "axios";

export const TokenStoreKey = "accessToken";


const clientId = "dd956ab9c89c4c2eb326b64a9191d3e9";
const clientSecret = "932cd57ceb4e4ce9be485754c48a011e";
const authorizationUri = "https://accounts.spotify.com";
const authorizationPath = "/authorize";
const tokenPath = "/api/token";

export const ProxyLink = "https://cors-anywhere.herokuapp.com/";

// tslint:disable-next-line:variable-name
let client!: oauth2.OAuthClient;

function GetClient(): oauth2.OAuthClient {
    //if (!client) {
    client = oauth2.create(new SpotifyCredentials());
    //}
    return client;
}

export interface IToken {
    access_token: string;
    token_type: string;
    scope: string;
    expires_in: number;
    refresh_token: string;
}

export class Token implements IToken {
    access_token: string;
    token_type: string;
    scope: string;
    expires_in: number;
    refresh_token: string;

    constructor(at: string, tt: string, s: string, ei: number, rt: string) {
        this.access_token = at;
        this.token_type = tt;
        this.scope = s;
        this.expires_in = ei;
        this.refresh_token = rt;
    }
}

export interface ISpotifyCredentials {
    client: {
        id: string,
        secret: string
    };
    auth: {
        tokenHost: string,
        tokenPath: string,
        authorizeHost: string,
        authorizePath: string
    };
    http: {
        headers: any
    };
}

export class SpotifyCredentials implements ISpotifyCredentials {
    client: any = {
        id: clientId,
        secret: clientSecret
    };
    auth: any = {
        tokenHost: ProxyLink + authorizationUri,
        tokenPath,
        authorizeHost: authorizationUri,
        authorizePath: authorizationPath
    };
    http: any = {
        headers: {
            "Access-Control-Request-Method": "OPTIONS,GET,POST",
            "Access-Control-Request-Origin": "*",
            "Access-Control-Request-Headers": "Origin,X-Requested-With",
            "Origin": "http://localhost:8080"
        }
    };
}

function CreateToken(result: any) {
    return GetClient().accessToken.create(result);
}
async function Authorize() {

    const authUri = GetClient().authorizationCode.authorizeURL({
        redirect_uri: "http://localhost:8080/callback",
        scope: "playlist-modify-public",
        state: Md5.hashStr("MuBotErFuckingInd").toString()
    });

    console.log(authUri);
    window.location.href = authUri;
    return;
}

async function GetToken(code: any, state?: any) {
    const body = {
        code,
        redirect_uri: "http://localhost:8080/callback",
        scope: "playlist-modify-public",
        grant_type: "authorization_code",
        clientId,
        clientSecret,
    };

    await axios({
        method: "post",
        url: ProxyLink + authorizationUri + tokenPath,
        data: body,
        headers: {
            "Authorization": "Basic " + btoa(clientId + ":" + clientSecret),
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
    }).then((tokenObj: any) => {
        const token: IToken = new Token(
            tokenObj.data["access_token"],
            tokenObj.data["token_type"],
            tokenObj.data["scope"],
            tokenObj.data["expires_in"],
            tokenObj.data["refresh_token"]);

        console.log(token);
        localStorage.setItem(TokenStoreKey, JSON.stringify(token));
        return;
    });
}

export default {
    Authorize,
    GetToken,
    CreateToken
};
