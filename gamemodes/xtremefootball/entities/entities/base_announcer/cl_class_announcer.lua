local Announcer = debug.getregistry().Announcer

net.Receive("Announcer:Say", function(length)
	local announcer = net.ReadEntity()

	if announcer:IsValid() then
		announcer:Say(net.ReadString())
	end
end)

function Announcer:Say(phrase)
	local name = self:GetPhraseSound(phrase)
	local properties = sound.GetProperties(name)

	if istable(properties) then
		if istable(properties.sound) then
			properties.sound = properties.sound[
			    math.random(#properties.sound)]
		end

		if istable(properties.pitch) then
			properties.pitch = math.Rand(
			    properties.pitch[1], properties.pitch[2])
		end
	else
		properties = {
			sound = name,
			pitch = 100,
		}
	end

	properties.sound = language.GetPhrase(properties.sound)

	self:EmitSound(properties.sound, 0, properties.pitch, 1, CHAN_VOICE2)
end
