import { Navbar } from "@/components/common/navbar";
import { UserProfile } from "@clerk/nextjs";
import { Fragment } from "react";


export default function Page(): React.JSX.Element {
    return (
        <Fragment>
            <Navbar />
            <main className="container mx-auto p-6 sm:py-8">
                <div className="flex justify-center items-center py-8">
                    <UserProfile path="/user-profile" />
                </div>
            </main>
        </Fragment>
    );
};