import { clerkMiddleware, createRouteMatcher } from '@clerk/nextjs/server';
import { NextRequest, NextResponse } from 'next/server';

const isDev = process.env.NODE_ENV === 'development';

const isPublicRoute = createRouteMatcher([
    '/',
    '/sign-in(.*)',
    '/sign-up(.*)'
]);

export default clerkMiddleware(async (auth, req: NextRequest) => {
    const { userId, redirectToSignIn } = await auth();
    const isPublic = isPublicRoute(req);

    if (isDev) {
        console.log("***============== Running in development mode. Skipping authentication checks. ==============***");
        // if (!userId && !isPublic) {
        //     return redirectToSignIn();
        // }
        // if (userId && isPublic) {
        //     return NextResponse.redirect(new URL('/dashboard', req.url));
        // }
    }
    else {
        console.log("***============== Running in production mode. Performing authentication checks. ==============***");
        if (!userId && !isPublic) {
            return redirectToSignIn();
        }
        if (userId && isPublic) {
            return NextResponse.redirect(new URL('/dashboard', req.url));
        }
    }
    return NextResponse.next();
});

export const config = {
    matcher: [
        // Skip Next.js internals and all static files, unless found in search params
        '/((?!_next|[^?]*\\.(?:html?|css|js(?!on)|jpe?g|webp|png|gif|svg|ttf|woff2?|ico|csv|docx?|xlsx?|zip|webmanifest)).*)',
        // Always run for API routes
        '/(api|trpc)(.*)',
        // Always run for Clerk-specific frontend API routes
        '/__clerk/(.*)',
    ],
}