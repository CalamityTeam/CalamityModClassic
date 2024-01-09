using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.NPCs.TheDevourerofGods
{
	public class DoGScreenShaderData : ScreenShaderData
	{
		private int DoGIndex;

		public DoGScreenShaderData(string passName)
			: base(passName)
		{
		}

		private void UpdateDoGIndex()
		{
			int DoGType = ModLoader.GetMod("CalamityModClassic1Point2").Find<ModNPC>("DevourerofGodsHead").Type;
			if (DoGIndex >= 0 && Main.npc[DoGIndex].active && Main.npc[DoGIndex].type == DoGType)
			{
				return;
			}
			DoGIndex = -1;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == DoGType)
				{
					DoGIndex = i;
					break;
				}
			}
		}

		public override void Apply()
		{
			UpdateDoGIndex();
			if (DoGIndex != -1)
			{
				UseTargetPosition(Main.npc[DoGIndex].Center);
			}
			base.Apply();
		}
	}
}