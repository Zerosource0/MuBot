import * as oauth2 from "simple-oauth2";
import Router from "../../ui/router/router";
import { Md5 } from "ts-md5/dist/md5";

const clientId = "dd956ab9c89c4c2eb326b64a9191d3e9";
const clientSecret = "932cd57ceb4e4ce9be485754c48a011e";
const authorizationUri = "https://accounts.spotify.com/authorize";

export interface ISpotifyCredentials {
    client: {
        id: string,
        secret: string
    };
    auth: {
        tokenHost: string
    };
}

export class SpotifyCredentials implements ISpotifyCredentials {
    client: any = {
        id: clientId,
        secret: clientSecret
    };
    auth: any = {
        tokenHost: authorizationUri
    };
}

async function Authorize(): Promise<oauth2.AccessToken | void> {

    const credentials = new SpotifyCredentials();

    const auth = oauth2.create(credentials);

    const authUri = auth.authorizationCode.authorizeURL({
        redirect_uri: "http://localhost:8080/callback",
        scope: "playlist-modify-public",
        state: Md5.hashStr("MuBotErFuckingInd").toString()
    });

    window.location.href = authUri;

    const tokenConfig = {
        code: "<code>",
        redirect_uri: "http://localhost:8080/main",
        scope: "playlist-modify-public",
    };

    try {
        const result = await auth.authorizationCode.getToken(tokenConfig);
        return auth.accessToken.create(result);
    } catch (error) {
        console.log("Access Token error:", error.message);
    }
}

export default {
    Authorize
};
