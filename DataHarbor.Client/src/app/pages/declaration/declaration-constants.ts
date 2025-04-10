import { ProcessMessage } from "../../../model/processMessage"
import { ProcessMessageStatus } from "../../../model/processMessageStatus"

export class ProcessStepMessage {
    constructor(public step: string, public message: ProcessMessageStatus) {
    }
}

export const ProcessingSteps = [
    new ProcessStepMessage('From Load Source', ProcessMessageStatus.Anchored),
    new ProcessStepMessage('From Process Data', ProcessMessageStatus.Docked),
    new ProcessStepMessage('From Update Accounts', ProcessMessageStatus.Adrifted),
]