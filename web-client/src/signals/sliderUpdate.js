// on slider movement

import config from "../config";

async function onUpdate(sessionObj) {
    await fetch(`${config.server}/sessions/${sessionObj.ProcessId}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(sessionObj)
    });
}

export default { onUpdate }
