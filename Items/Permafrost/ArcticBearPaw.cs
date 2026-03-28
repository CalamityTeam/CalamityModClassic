using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class ArcticBearPaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Arctic Bear Paw");
			/* Tooltip.SetDefault(@"Fires spiritual claws that ignore walls and confuse enemies
'The savage mauling that fits in your pocket'"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 70;
			Item.DamageType = DamageClass.Magic;
            Item.mana = 18;
			Item.width = 34;
			Item.height = 22;
			Item.useTime = 28;
            Item.useAnimation = 28;
			Item.useStyle = 5;
			Item.useTurn = false;
			Item.noMelee = true;
			Item.knockBack = 10f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("ArcticBearPaw").Type;
            Item.shootSpeed = 27f;
		}
    }
}
