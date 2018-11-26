import { namespace } from "vuex-class";

// tslint:disable-next-line:max-line-length
export const bindToNamespace = (namespaceName: string) => (bindingHelper: any) => namespace(namespaceName, bindingHelper);
