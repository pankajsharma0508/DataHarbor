import { ProcessMessage } from "../../../model/processMessage"
import { ProcessMessageStatus } from "../../../model/processMessageStatus"

export class ProcessStepMessage {
    constructor(public step: string, public message: ProcessMessageStatus) {
    }
}

export const ProcessingSteps = [
    new ProcessStepMessage('From Anchored', ProcessMessageStatus.Anchored),
    new ProcessStepMessage('From Docked', ProcessMessageStatus.Docked),
    new ProcessStepMessage('From Adrifted', ProcessMessageStatus.Adrifted),
]