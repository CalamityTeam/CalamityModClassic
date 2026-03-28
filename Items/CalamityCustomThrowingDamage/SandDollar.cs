using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage
{
	public class SandDollar : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sand Dollar");
			// Tooltip.SetDefault("Stacks up to 2");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.damage = 30;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 1;
			Item.maxStack = 2;
			Item.knockBack = 3.5f;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item1;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = 2;
			Item.shoot = Mod.Find<ModProjectile>("SandDollarProj").Type;
			Item.shootSpeed = 14f;
			Item.GetGlobalItem<CalamityGlobalItem>().rogue = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			int MAX = Item.stack;
		    int launched = 0;
		    foreach (Projectile projectile in Main.projectile)
		    if (projectile.type == Item.shoot && projectile.owner == Item.playerIndexTheItemIsReservedFor && projectile.active)
			{	
		        launched++;
			}
		    return (launched >= MAX) ? false : true;
		}
	}
}
