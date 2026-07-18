import Link from "next/link";
import { ShieldAlert, Home } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";

export default function ForbiddenPage() {
    return (
        <main className="container mx-auto px-4 py-10 sm:py-16">
            <Card className="mx-auto max-w-xl">
                <CardHeader className="text-center">
                    <div className="mx-auto mb-4 flex h-12 w-12 items-center justify-center rounded-full bg-amber-100">
                        <ShieldAlert className="h-6 w-6 text-amber-700" />
                    </div>
                    <CardTitle className="text-2xl">Forbidden (403)</CardTitle>
                    <CardDescription>
                        You are authenticated, but your account is not allowed to access this resource.
                    </CardDescription>
                </CardHeader>
                <CardContent className="flex justify-center gap-3">
                    <Button asChild>
                        <Link href="/dashboard">Go to Dashboard</Link>
                    </Button>
                    <Button variant="outline" asChild>
                        <Link href="/">
                            <Home className="mr-2 h-4 w-4" />
                            Back Home
                        </Link>
                    </Button>
                </CardContent>
            </Card>
        </main>
    );
}
