using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Providence {
public class BlissfulBombardier : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Blissful Bombardier");
		//Tooltip.SetDefault("Fires flare rockets");
	}

    public override void SetDefaults()
    {
        Item.damage = 200;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 66;
        Item.height = 28;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7.5f;
        Item.value = 5000000;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shootSpeed = 24f;
        Item.shoot = Mod.Find<ModProjectile>("Nuke").Type;
        Item.useAmmo = 771;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 200);
            }
        }
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Nuke").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}