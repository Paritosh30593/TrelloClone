"use client"

import * as React from "react"
import { ToggleGroup as ToggleGroupPrimitive } from "radix-ui"
import { cn } from "@/lib/utils"
import style from "./toggle-group.module.css"

function ToggleGroup({
    className,
    ...props
}: React.ComponentProps<typeof ToggleGroupPrimitive.Root>) {
    return (
        <ToggleGroupPrimitive.Root
            data-slot="toggle-group"
            className={cn(`inline-flex items-center ${style["ToggleGroup"]}`, className)}
            {...props}
        />
    )
}

function ToggleGroupItem({
    className,
    ...props
}: React.ComponentProps<typeof ToggleGroupPrimitive.Item>) {
    return (
        <ToggleGroupPrimitive.Item
            data-slot="toggle-group-item"
            className={cn(
                `inline-flex h-8 items-center justify-center rounded-lg border border-input bg-transparent px-3 text-sm font-medium whitespace-nowrap transition-colors hover:bg-purple-200 hover:text-foreground focus-visible:border-ring focus-visible:ring-3 focus-visible:ring-ring/50 focus-visible:outline-none disabled:pointer-events-none disabled:opacity-50 data-[state=on]:text-primary-foreground dark:border-input data-[state=on]:bg-purple-500 ${style["ToggleGroupItem"]}`,
                className
            )}
            {...props}
        />
    )
}

export { ToggleGroup, ToggleGroupItem }