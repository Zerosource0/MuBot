declare module "requests" {
	namespace Request {
		interface createTokenRequest {
			code: string;
			redirectUri: string;
		}
		interface loginRequest {
			username: string;
			password: string;
		}
		interface createUserRequest {
			firstName: string;
			lastName: string;
			username: string;
			password: string;
			email: string;
		}
	}
	export = Request;
}
