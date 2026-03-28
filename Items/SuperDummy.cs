using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
	public class SuperDummy : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Super Dummy");
			/* Tooltip.SetDefault("Creates a super dummy\n" +
                "Regenerates 1 million life per second\n" +
                "Will not die when taking damage over time from debuffs\n" +
                "Right click to kill all super dummies"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.width = 20;
			Item.height = 30;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.useTurn = true;
            Item.value = 0;
			Item.rare = 1;
			Item.autoReuse = true;
		}

		public override bool AltFunctionUse (Player player)
		{
			return true;
		}

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            if (player.altFunctionUse == 2)
			{
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].type == Mod.Find<ModNPC>("SuperDummy").Type)
					{
						Main.npc[i].life = 0;
						Main.npc[i].lifeRegen = 0;
						Main.npc[i].checkDead();
					}
				}
			}
			else if (player.whoAmI == Main.myPlayer)
			{
				int x = (int) Main.MouseWorld.X - 9;
				int y = (int) Main.MouseWorld.Y - 20;
				NPC.NewNPC(new EntitySource_ItemUse(player, Item),x, y, Mod.Find<ModNPC>("SuperDummy").Type);
			}
            return true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TargetDummy);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}
