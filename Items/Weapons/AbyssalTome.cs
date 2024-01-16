using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AbyssalTome : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/4.SlimeGod/AbyssalTome");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Abyssal Tome");
        Item.damage = 40;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 15;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts a slow-moving ball of dark energy");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 6;
        Item.value = 150000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item8;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("AbyssBall").Type;
        Item.shootSpeed = 9f;
    }
}}