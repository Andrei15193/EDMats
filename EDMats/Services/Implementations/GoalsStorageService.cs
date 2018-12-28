using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EDMats.Services.Implementations
{
    public class GoalsStorageService : IGoalsStorageService
    {
        public Task<CommanderGoalsData> ReadGoalsAsync(TextReader textReader)
            => ReadGoalsAsync(textReader, CancellationToken.None);

        public async Task<CommanderGoalsData> ReadGoalsAsync(TextReader textReader, CancellationToken cancellationToken)
        {
            if (textReader == null)
                throw new ArgumentNullException(nameof(textReader));

            IReadOnlyCollection<MaterialGoalData> materialsGoal = null;
            using (var jsonTextReader = new JsonTextReader(textReader))
            {
                await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
                if (jsonTextReader.TokenType == JsonToken.StartObject)
                {
                    await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
                    do
                    {
                        _Expected(jsonTextReader, JsonToken.PropertyName);
                        switch ((string)jsonTextReader.Value)
                        {
                            case "materials":
                                materialsGoal = await _ReadMaterialsGoalAsync(jsonTextReader, cancellationToken).ConfigureAwait(false);
                                await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
                                break;

                            default:
                                do
                                {
                                    if (jsonTextReader.TokenType == JsonToken.StartObject || jsonTextReader.TokenType == JsonToken.StartArray)
                                        await jsonTextReader.SkipAsync(cancellationToken).ConfigureAwait(false);
                                    await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
                                } while (jsonTextReader.TokenType != JsonToken.PropertyName && jsonTextReader.TokenType != JsonToken.EndObject);
                                break;
                        }
                    } while (jsonTextReader.TokenType != JsonToken.None && jsonTextReader.TokenType != JsonToken.EndObject);
                }
                else if (jsonTextReader.TokenType != JsonToken.None)
                    _Expected(jsonTextReader, JsonToken.StartObject);
            }

            return new CommanderGoalsData
            {
                Materials = materialsGoal ?? new List<MaterialGoalData>()
            };
        }

        private static async Task<IReadOnlyCollection<MaterialGoalData>> _ReadMaterialsGoalAsync(JsonTextReader jsonTextReader, CancellationToken cancellationToken)
        {
            var materialsGoal = new List<MaterialGoalData>();

            await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
            if (jsonTextReader.TokenType == JsonToken.StartArray)
            {
                await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
                while (jsonTextReader.TokenType != JsonToken.EndArray)
                    materialsGoal.Add(await _ReadMaterialGoalAsync(jsonTextReader, cancellationToken).ConfigureAwait(false));
                await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
            }
            await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);

            return materialsGoal;
        }

        private static async Task<MaterialGoalData> _ReadMaterialGoalAsync(JsonTextReader jsonTextReader, CancellationToken cancellationToken)
        {
            var materialGoalData = new MaterialGoalData();

            _Expected(jsonTextReader, JsonToken.StartObject);
            await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
            while (jsonTextReader.TokenType != JsonToken.EndObject)
            {
                _Expected(jsonTextReader, JsonToken.PropertyName);
                switch ((string)jsonTextReader.Value)
                {
                    case "name":
                        materialGoalData.MaterialId = await jsonTextReader.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                        materialGoalData.Name = Materials.FindById(materialGoalData.MaterialId).Name;
                        await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case "count":
                        materialGoalData.Amount = await jsonTextReader.ReadAsInt32Async(cancellationToken).ConfigureAwait(false) ?? 0;
                        await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    default:
                        do
                        {
                            if (jsonTextReader.TokenType == JsonToken.StartObject || jsonTextReader.TokenType == JsonToken.StartArray)
                                await jsonTextReader.SkipAsync(cancellationToken).ConfigureAwait(false);
                            await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);
                        } while (jsonTextReader.TokenType != JsonToken.PropertyName && jsonTextReader.TokenType != JsonToken.EndObject);
                        break;
                }
            }
            await jsonTextReader.ReadAsync(cancellationToken).ConfigureAwait(false);

            return materialGoalData;
        }

        private static void _Expected(JsonTextReader jsonTextReader, JsonToken jsonToken)
        {
            if (jsonTextReader.TokenType != jsonToken)
                throw new InvalidOperationException($"Expected '{jsonToken}', actually '{jsonTextReader.TokenType }' was read.");
        }

        public Task WriteGoalsAsync(TextWriter textWriter, CommanderGoalsData commanderGoalsData)
            => WriteGoalsAsync(textWriter, commanderGoalsData, CancellationToken.None);

        public async Task WriteGoalsAsync(TextWriter textWriter, CommanderGoalsData commanderGoalsData, CancellationToken cancellationToken)
        {
            if (textWriter == null)
                throw new ArgumentNullException(nameof(textWriter));
            if (commanderGoalsData == null)
                throw new ArgumentNullException(nameof(commanderGoalsData));

            using (var jsonTextWriter = new JsonTextWriter(textWriter))
            {
                await jsonTextWriter.WriteStartObjectAsync(cancellationToken).ConfigureAwait(false);

                await _WriteMaterialsGoalAsync(commanderGoalsData, jsonTextWriter, cancellationToken).ConfigureAwait(false);

                await jsonTextWriter.WriteEndObjectAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static async Task _WriteMaterialsGoalAsync(CommanderGoalsData commanderGoalsData, JsonTextWriter jsonTextWriter, CancellationToken cancellationToken)
        {
            await jsonTextWriter.WritePropertyNameAsync("materials", cancellationToken).ConfigureAwait(false);
            await jsonTextWriter.WriteStartArrayAsync(cancellationToken).ConfigureAwait(false);
            foreach (var materialGoal in commanderGoalsData.Materials)
                if (materialGoal.Amount > 0)
                    await _WriteMaterialGoalAsync(jsonTextWriter, materialGoal, cancellationToken).ConfigureAwait(false);
            await jsonTextWriter.WriteEndArrayAsync(cancellationToken).ConfigureAwait(false);
        }

        private static async Task _WriteMaterialGoalAsync(JsonTextWriter jsonTextWriter, MaterialGoalData materialGoal, CancellationToken cancellationToken)
        {
            await jsonTextWriter.WriteStartObjectAsync(cancellationToken).ConfigureAwait(false);

            await jsonTextWriter.WritePropertyNameAsync("name", cancellationToken).ConfigureAwait(false);
            await jsonTextWriter.WriteValueAsync(materialGoal.MaterialId, cancellationToken).ConfigureAwait(false);

            await jsonTextWriter.WritePropertyNameAsync("count", cancellationToken).ConfigureAwait(false);
            await jsonTextWriter.WriteValueAsync(materialGoal.Amount, cancellationToken).ConfigureAwait(false);

            await jsonTextWriter.WriteEndObjectAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}