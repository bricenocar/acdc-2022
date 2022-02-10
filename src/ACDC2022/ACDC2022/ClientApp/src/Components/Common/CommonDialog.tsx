import React, { FC } from 'react';
import { Dialog, DialogType, DialogFooter } from '@fluentui/react/lib/Dialog';
import { PrimaryButton, DefaultButton } from '@fluentui/react/lib/Button';
import { ContextualMenu } from '@fluentui/react/lib/ContextualMenu';
import { useId, useBoolean } from '@fluentui/react-hooks';
import { Stack } from '@fluentui/react';

interface CommonDialogProps {
    hideDialog: boolean;
    maxWidth: number;
    subText: string;
    textCancel: string;
    textOk: string;
    title: string;
    cancelEvent: () => void;
    okEvent: () => void;
}

export const CommonDialog: FC<CommonDialogProps> = ({ hideDialog, title, subText, maxWidth, textOk, textCancel, okEvent, cancelEvent }) => {
    const [isDraggable] = useBoolean(false);
    const labelId: string = useId('dialogLabel');
    const subTextId: string = useId('subTextLabel');

    const dialogStyles = { main: { maxWidth } };
    const dragOptions = {
        moveMenuItemText: 'Move',
        closeMenuItemText: 'Close',
        menu: ContextualMenu,
        keepInBounds: true,
    };
    const dialogContentProps = {
        type: DialogType.normal,
        title,
        closeButtonAriaLabel: 'Close',
        subText,
    };
    const modalProps = React.useMemo(
        () => ({
            titleAriaId: labelId,
            subtitleAriaId: subTextId,
            isBlocking: false,
            styles: dialogStyles,
            dragOptions: isDraggable ? dragOptions : undefined,
        }),
        [isDraggable, labelId, subTextId],
    );

    return (
        <Stack>
            <Dialog
                hidden={hideDialog}
                onDismiss={cancelEvent}
                dialogContentProps={dialogContentProps}
                modalProps={modalProps}
            >
                <DialogFooter>
                    <PrimaryButton onClick={okEvent} text={textOk} />
                    <DefaultButton onClick={cancelEvent} text={textCancel} />
                </DialogFooter>
            </Dialog>
        </Stack>
    );
};
