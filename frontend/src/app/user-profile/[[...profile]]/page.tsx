import { Navbar } from "@/components/common/navbar";
import { UserProfile } from "@clerk/nextjs";
import { Fragment } from "react";


export default function Page(): React.JSX.Element {
    return (
        <Fragment>
            <Navbar />
            <div className="flex justify-center items-center py-8">
                <UserProfile path="/user-profile" />
            </div>
        </Fragment>
    );
};