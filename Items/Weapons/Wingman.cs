using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Wingman : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Wingman");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Wingman");
        Item.damage = 50;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 12;
        Item.width = 32;
        Item.height = 22;
        ////Tooltip.SetDefault("Fires a concentrated laser bolt");
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.5f;
        Item.value = 500000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item33;
        Item.autoReuse = true;
        Item.shootSpeed = 25f;
        Item.shoot = 440;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int num6 = 3;
        for (int index = 0; index < num6; ++index)
        {
            Projectile.NewProjectile(source, position, velocity,440, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        return false;
	}
}}