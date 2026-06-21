import { UserProfile } from "@clerk/nextjs";


export default function Page(): React.JSX.Element {
    return (
        <div className="flex justify-center items-center py-8">
            <UserProfile path="/user-profile" />
        </div>
    );
};