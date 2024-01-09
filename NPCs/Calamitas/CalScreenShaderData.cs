using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.NPCs.Calamitas
{
	public class CalScreenShaderData : ScreenShaderData
	{
		private int CalIndex;

		public CalScreenShaderData(string passName)
			: base(passName)
		{
		}

		private void UpdateCalIndex()
		{
			int CalType = ModLoader.GetMod("CalamityModClassic1Point2").Find<ModNPC>("CalamitasRun3").Type;
			if (CalIndex >= 0 && Main.npc[CalIndex].active && Main.npc[CalIndex].type == CalType)
			{
				return;
			}
			CalIndex = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == CalType)
				{
					CalIndex = i;
					break;
				}
			}
		}

		public override void Apply()
		{
			UpdateCalIndex();
			if (CalIndex != -1)
			{
				UseTargetPosition(Main.npc[CalIndex].Center);
			}
			base.Apply();
		}
	}
}