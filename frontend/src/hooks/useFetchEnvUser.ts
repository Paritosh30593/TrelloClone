import { useUser } from "@clerk/nextjs";

export const useFetchEnvUser = () => {
    const { user: prodUser, isSignedIn: isProdSignedIn } = useUser();
    const devUser = {
        id: "user_3ERh5RTe2wMLi97eVnynpW8nQ3y",
        firstName: "Paritosh",
        lastName: "Desai"
    };

    return process.env.NODE_ENV === "production"
        ? {
            isSignedIn: isProdSignedIn,
            user: prodUser
        }
        : {
            isSignedIn: true,
            user: devUser
        };
};