import { useMsal } from "@azure/msal-react";

export const useFetchEnvUser = () => {
    const { accounts } = useMsal();
    const account = accounts[0];

    if (!account) {
        return { isSignedIn: false, user: null };
    }

    return {
        isSignedIn: true,
        user: {
            id: account.localAccountId,
            firstName: account.name?.split(" ")[0] ?? "",
            lastName: account.name?.split(" ").slice(1).join(" ") ?? ""
        }
    };
};